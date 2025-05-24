using System;

public interface IDamageHandler
{
    public event EventHandler OnHealthIsZero;
    public void ApplyDamage(float amount);

}