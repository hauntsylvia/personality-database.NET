using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using personality_helper.Classes;
using personality_helper;
using personality_helper.Classes.MBTI;
using personality_helper.Classes.Enneagram;

namespace personality_database.NET.Classes.Entities.Models
{
    /// <summary>
    /// the <see cref="FullPersonality"/> of this class is not fully implemented to correctly reflect the api's use-case of this object 
    /// (missing specific attributes)
    /// </summary>
    public class Profile : SearchProfile
    {
        [JsonConstructor]
        public Profile(ulong id, string profileImageUrl, string subcategory, string profileName, string unparsedFullPersonality, string wikiDescription) : base(id, profileImageUrl, subcategory, profileName, unparsedFullPersonality)
        {
            /*expected unparsed string is ``XXXX - XwX`` {not implemented}*/
            this._fullPersonality = new((MyersBriggsResult)unparsedFullPersonality.Split(' ')[0], (Enneagram)unparsedFullPersonality.Split(' ')[2].Split(' ')[0]);

            this._wikiDescription = wikiDescription;
        }
        #region new stuff separate from base
        private readonly string _wikiDescription;
        [JsonProperty("wiki_description")]
        public string wikiDescription => _wikiDescription;
        #endregion
        #region json serializers responsibility (ignoring or overriding base)
        private readonly FullPersonality _fullPersonality;
        public new FullPersonality fullPersonality => _fullPersonality;
        #endregion
    }
}
