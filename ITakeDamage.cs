using UnityEngine;

// varje monobehaviour som implementerar denna interfacet kommer plokckas upp av vår projektile och OnCollideTakeDamage implementeras
public interface ITakeDamage {

    void TakeDamage(int damage, GameObject instigator);

}

