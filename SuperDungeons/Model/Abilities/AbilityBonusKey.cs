namespace SuperDungeons.Model.Abilities;


//ReSharper disable once NotAccessedPositionalProperty.Global
//Source is only used in Equals to Compare Instances, this is how it is intended
internal record AbilityBonusKey(Ability Ability, string Source);