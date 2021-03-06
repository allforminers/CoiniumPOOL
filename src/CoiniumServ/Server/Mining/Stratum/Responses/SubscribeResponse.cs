#region License
// 
//     MIT License
//
//     CoiniumServ - Crypto Currency Mining Pool Server Software
//     Copyright (C) 2013 - 2017, CoiniumServ Project
//     Hüseyin Uslu, shalafiraistlin at gmail dot com
//     https://github.com/bonesoul/CoiniumServ
// 
//     Permission is hereby granted, free of charge, to any person obtaining a copy
//     of this software and associated documentation files (the "Software"), to deal
//     in the Software without restriction, including without limitation the rights
//     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//     copies of the Software, and to permit persons to whom the Software is
//     furnished to do so, subject to the following conditions:
//     
//     The above copyright notice and this permission notice shall be included in all
//     copies or substantial portions of the Software.
//     
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//     SOFTWARE.
// 
#endregion

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace CoiniumServ.Server.Mining.Stratum.Responses
{
    [JsonArray]
    public class SubscribeResponse:IEnumerable<object>
    {
        /// <summary>
        /// Used for building the coinbase.
        /// <remarks>Hex-encoded, per-connection unique string which will be used for coinbase serialization later. Keep it safe! (http://mining.bitcoin.cz/stratum-mining)</remarks>
        /// </summary>
        [JsonIgnore]   
        public string ExtraNonce1 { get; set; }

        /// <summary>
        /// The number of bytes that the miner users for its ExtraNonce2 counter 
        /// <remarks>Represents expected length of extranonce2 which will be generated by the miner. (http://mining.bitcoin.cz/stratum-mining)</remarks>
        /// </summary>
        [JsonIgnore]   
        public int ExtraNonce2Size { get; set; }

        /// <summary>
        /// The miner's subscription id.
        /// </summary>
        [JsonIgnore]
        public int SubscriptionId { get; set; }

        public IEnumerator<object> GetEnumerator()
        {
            var data = new List<object>
            {
                // 2-tuple with name of subscribed notification and subscription ID. Teoretically it may be used for unsubscribing, 
                // but obviously miners won't use it. (http://mining.bitcoin.cz/stratum-mining)
                new List<string>
                {
                    "mining.set_difficulty",
                    SubscriptionId.ToString(CultureInfo.InvariantCulture), // set to miner's id - just a place holder value.
                    "mining.notify", 
                    SubscriptionId.ToString(CultureInfo.InvariantCulture) // set to miner's id - just a place holder value.
                },
                ExtraNonce1, // send the ExtraNonce1
                ExtraNonce2Size // expected length of extranonce2 which will be generated by the miner
            };

            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
