using System.Runtime.Serialization;

namespace CPL.Models.Dto
{
    [DataContract]
    public class Market
    {
        [DataMember(Name = "marketName")]
        public string MarketName { get; set; }

        [DataMember(Name = "marketUrl")]
        public string MarketUrl { get; set; }

        [DataMember(Name = "twentyFourHourVolume")]
        public string TwentyFourHourVolume { get; set; }
    }
}