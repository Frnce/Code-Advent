namespace Advent.Utilities
{
    public enum EquipSlot
    {
        HEAD,
        BODY,
        ACCESORY, //REMOVE
        WEAPON, //REMOVE
        MAINHAND,
        OFFHAND,
        RING,
        NECKLACE
    }
    public enum ItemType
    {
        EQUIPMENT,
        CONSUMABLE
    }
    public enum WeaponType
    {
        ONEHANDED,
        TWOHANDED,
        RANGE
    }
    public enum EquipmentRarity
    {
        COMMON, //WHITE
        RARE, //BLUE
        EPIC, //PURPLE / VIOLET
        LEGENDARY, // YELLOW / ORANGE
        GOD // RED
    }
    public enum EquipmentType
    {
        NORMAL, // WHITE
        SET,
        CURSED,
        DIVINE
    }
    public enum TileStatus
    {
        EMPTY,
        GROUND,
        OBJECTS,
        WALL,
        PLAYER,
        ENEMY
    }
}