/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using System.Collections.Generic;

namespace Meta.WitAi.Requests
{
    internal class WitMessageVRequest : WitVRequest
    {
        // Partial response handler
        private RequestCompleteDelegate<string> _onPartial;

        /// <summary>
        /// Constructor for wit based message VRequests
        /// </summary>
        /// <param name="configuration">The configuration interface to be used</param>
        /// <param name="requestId">A unique identifier that can be used to track the request</param>
        /// <param name="onDownloadProgress">The callback for progress related to downloading</param>
        /// <param name="onFirstResponse">The callback for the first response of data from a request</param>
        public WitMessageVRequest(IWitRequestConfiguration configuration, string requestId,
            RequestProgressDelegate onDownloadProgress = null,
            RequestFirstResponseDelegate onFirstResponse = null,
            RequestCompleteDelegate<string> onPartial = null)
            : base(configuration, requestId, false, onDownloadProgress, onFirstResponse)
        {
            _onPartial = onPartial;
        }

        /// <summary>
        /// Voice message request
        /// </summary>
        /// <param name="text">Text to be sent to message endpoint</param>
        /// <param name="queryParams">Parameters to be sent to the endpoint</param>
        /// <param name="onComplete">The callback delegate on request completion</param>
        /// <returns>False if the request cannot be performed</returns>
        public bool MessageRequest(string text,
            Dictionary<string, string> queryParams,
            RequestCompleteDelegate<string> onComplete,
            RequestCompleteDelegate<string> onPartial = null) =>
            MessageRequest(WitConstants.ENDPOINT_MESSAGE, false, text, queryParams, onComplete, onPartial);

        /// <summary>
        /// Voice message request
        /// </summary>
        /// <param name="endpoint">Endpoint to be used for possible overrides</param>
        /// <param name="post">Will perform a POST if true, will perform a GET otherwise</param>
        /// <param name="text">Text to be sent to message endpoint</param>
        /// <param name="queryParams">Parameters to be sent to the endpoint</param>
        /// <param name="onComplete">The callback delegate on request completion</param>
        /// <returns>False if the request cannot be performed</returns>
        public bool MessageRequest(string endpoint, bool post, string text,
            Dictionary<string, string> queryParams,
            RequestCompleteDelegate<string> onComplete,
            RequestCompleteDelegate<string> onPartial = null)
        {
            // Add text to uri parameters
            Dictionary<string, string> uriParams = queryParams ?? new Dictionary<string, string>();

            // Perform a get request
            if (!post)
            {
                uriParams[WitConstants.ENDPOINT_MESSAGE_PARAM] = text;
                return RequestWitGet(endpoint, uriParams, onComplete, onPartial == null ? _onPartial : onPartial);
            }
            // Perform a post request
            else
            {
                return RequestWitPost(endpoint, uriParams, text, onComplete, onPartial == null ? _onPartial : onPartial);
            }
        }
    }
}
