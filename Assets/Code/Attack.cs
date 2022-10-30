
public class Attack 
{
    public AttackBase Base { get; set; }
    public int Cost { get; set; }

    public Attack(AttackBase _base)
    {
        Base = _base;
        Cost = _base.Cost;
    }
}