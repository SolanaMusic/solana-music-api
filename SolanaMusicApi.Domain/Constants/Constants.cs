using SolanaMusicApi.Domain.Enums.Nft;

namespace SolanaMusicApi.Domain.Constants;

public static class Constants
{
    public const string SystemAddress = "7eJ1xj1EKK3nZPP4AkUHeMikD2L2Q7HMMZPPb5uhQ1zq";
    
    public static readonly Dictionary<Rarity, double> RarityProbabilities = new()
    {
        { Rarity.Common, 0.95 },
        { Rarity.Rare, 0.04 },
        { Rarity.Epic, 0.006 },
        { Rarity.Mythic, 0.003 },
        { Rarity.Legendary, 0.001 }
    };
}