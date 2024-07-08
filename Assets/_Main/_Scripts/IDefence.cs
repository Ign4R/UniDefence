interface IDefence
{
    bool UpgradeMax { get; set; }
    bool IsActive { get; set; }
    void UpgradeDefence();
}