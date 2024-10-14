/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Text;
using System.Collections.Generic;
using Meta.Voice;
using Meta.WitAi.Composer.Interfaces;
using Meta.WitAi.Json;
using Meta.WitAi.Requests;

namespace Meta.WitAi.Composer.Integrations
{
    public class WitComposerRequestHandler : System.Object, IComposerRequestHandler
    {
        // Configuration data
        private IWitRequestConfiguration _configuration;

        /// <summary>
        /// Overrides request uri callback to handle composer based request
        /// </summary>
        /// <param name="request">Request used to perform composer functionality if desired</param>
        /// <param name="sessionData">Session data including composer, voice service, and more</param>
        public void OnComposerRequestSetup(ComposerSessionData sessionData, VoiceServiceRequest request)
        {
            // Invalid
            if (request == null || sessionData.composer == null || sessionData.composer.VoiceService == null)
            {
                return;
            }

            // Get configuration
            _configuration = sessionData.composer.VoiceService.WitConfiguration;

            // Add parameters
            request.Options.QueryParams[WitComposerConstants.ENDPOINT_COMPOSER_PARAM_SESSION] = sessionData.sessionID;
            request.Options.QueryParams[WitComposerConstants.ENDPOINT_COMPOSER_MESSAGE_TAG] = sessionData.versionTag;

            if (sessionData.composer.VoiceService.UsePlatformIntegrations)
            {
                request.Options.QueryParams[WitComposerConstants.PI_COMPOSER_ENABLE] =
                    WitComposerConstants.PI_COMPOSER_ENABLE_ON;
            }

            // Event adjustments
            bool messageIsEmpty = false;
            string message = null;
            if (request.InputType == NLPRequestInputType.Text)
            {
                // Default to raw message
                message = request.Options.Text;

                // Determine if event state
                bool isEvent = sessionData.composer.IsEventJson(message);

                // If not event or empty, serialize into message json
                messageIsEmpty = string.IsNullOrEmpty(message);
                if (!isEvent || messageIsEmpty)
                {
                    if (request is WitSocketRequest)
                    {
                        request.Options.QueryParams[WitComposerConstants.ENDPOINT_COMPOSER_MESSAGE_PARAM_MESSAGE] = message;
                        request.Options.QueryParams[WitComposerConstants.ENDPOINT_COMPOSER_MESSAGE_PARAM_TYPE] =
                            WitComposerMessageType.Message.ToString().ToLower();
                    }
                    else
                    {
                        Dictionary<string, string> contents = new Dictionary<string, string>();
                        contents[WitComposerConstants.ENDPOINT_COMPOSER_MESSAGE_PARAM_MESSAGE] = message;
                        contents[WitComposerConstants.ENDPOINT_COMPOSER_MESSAGE_PARAM_TYPE] =
                            WitComposerMessageType.Message.ToString().ToLower();
                        message = JsonConvert.SerializeObject(contents);
                    }
                }

                // Update message text
                request.Options.Text = message;

                // Remove message key if possible
                if (request.Options.QueryParams.ContainsKey(WitConstants.ENDPOINT_MESSAGE_PARAM))
                {
                    request.Options.QueryParams.Remove(WitConstants.ENDPOINT_MESSAGE_PARAM);
                }
            }

            //Finally, update the context_map event flag and add to parameters.
            sessionData.contextMap?.SetData(sessionData.composer.contextMapEventKey,messageIsEmpty.ToString().ToLower());
            request.Options.QueryParams[WitComposerConstants.ENDPOINT_COMPOSER_PARAM_CONTEXT_MAP] = sessionData.contextMap?.GetJson();
            request.Options.QueryParams[WitComposerConstants.ENDPOINT_COMPOSER_PARAM_DEBUG] = sessionData.contextMap
                .Data[WitComposerConstants.ENDPOINT_COMPOSER_PARAM_DEBUG].AsBool ? "true" : "false";

            // Adjust path on WitRequest & append text if desired
            if (request is WitRequest wr)
            {
                wr.Path = GetEndpointPath(request.InputType);
                if (request.InputType == NLPRequestInputType.Text)
                {
                    wr.postContentType = "application/json";
                    wr.postData = Encoding.UTF8.GetBytes(message);
                }
            }
            // Adjust path & settings on WitUnityRequest
            else if (request is WitUnityRequest wur)
            {
                wur.Endpoint = GetEndpointPath(request.InputType);
                wur.ShouldPost = true;
            }
            // Adjust endpoint for socket requests
            else if (request is WitSocketRequest wsr)
            {
                wsr.Endpoint = GetEndpointPath(request.InputType);
            }
        }

        // Helper for endpoint path override
        private string GetEndpointPath(NLPRequestInputType inputType)
        {
            if (inputType == NLPRequestInputType.Audio)
            {
                return _configuration == null ? WitComposerConstants.ENDPOINT_COMPOSER_SPEECH : _configuration.GetEndpointInfo().Converse;
            }
            if (inputType == NLPRequestInputType.Text)
            {
                return _configuration == null ? WitComposerConstants.ENDPOINT_COMPOSER_MESSAGE : _configuration.GetEndpointInfo().Event;
            }
            VLog.E($"Unsupported input type: {inputType}");
            return null;
        }
    }
}