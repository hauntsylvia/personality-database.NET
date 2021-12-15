using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using personality_helper;
using personality_helper.Classes;
using personality_helper.Classes.MBTI;
using personality_helper.Classes.Enneagram;

namespace personality_database.NET.Classes.Entities.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class SearchProfile : IPBEntity
    {
        [JsonConstructor]
        internal SearchProfile(ulong id, string profileImageUrl, string subcategory, string profileName, string unparsedFullPersonality)
        {
            this._id = id;
            this._uri = uri;
            this._profileImageUrl = profileImageUrl;
            this._subcategory = subcategory;
            this._profileName = profileName;
            this._unparsedFullPersonality = unparsedFullPersonality;
            string e1u = unparsedFullPersonality.Contains('w') ? unparsedFullPersonality.Split('w', 2)[0].Split(' ')[1] : "";
            string e2u = unparsedFullPersonality.Contains('w') ? unparsedFullPersonality.Split('w', 2)[1].Split(' ')[0] : "";
            Enneagram enn = int.TryParse(e1u, out int e1) && int.TryParse(e2u, out int e2) ? new(e1, e2, false) : new(-1, -1, true);
            MyersBriggsResult mbti = new(unparsedFullPersonality.Split(' ')[0], true);
            this._fullPersonality = new FullPersonality(mbti, enn);
        }
        public SearchProfile(ulong id, string profileImageUrl, string subcategory, string profileName, FullPersonality personality)
        {
            this._id = id;
            this._uri = uri;
            this._profileImageUrl = profileImageUrl;
            this._subcategory = subcategory;
            this._profileName = profileName;
            this._fullPersonality = personality;
        }

        #region ur handling of to-use properties || ur handling, serializers responsibility to conform
        /// <summary>
        /// dont use this literally ever unless u want json errors
        /// </summary>
        /// <remarks>
        /// will return a hellspawn
        /// </remarks>
        private readonly string? _unparsedFullPersonality;
        [JsonProperty("personality_type")]
        public string unparsedFullPersonality => _unparsedFullPersonality ?? string.Empty;


        private readonly string _uri;
        public string uri => _uri;
        #endregion

        #region serializers responsibility at default || to-use properties
        private readonly ulong _id;
        [JsonProperty("id")]
        public ulong id => _id;


        private readonly string _profileImageUrl;
        [JsonProperty("profile_image_url")]
        public string profileImageUrl => _profileImageUrl;


        private readonly string _subcategory;
        [JsonProperty("subcategory")]
        public string subcategory => _subcategory;


        private readonly string _profileName;
        [JsonProperty("mbti_profile")]
        public string profileName => _profileName;


        private readonly FullPersonality _fullPersonality;
        public FullPersonality fullPersonality => _fullPersonality;
        #endregion
    }
}
