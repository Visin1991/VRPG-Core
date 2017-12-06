using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     AiInfomationRecive is used to recive information.
/// For example when the player get into certain range, the player
/// will tell the Ai---player is comming.
///     The reason I dont use the Ai itself to check the distance to
/// the player, Because When there is alot of Ai on the scene, if all of
/// they doing a Physics or whatever check, It could cost a lot of performance.
/// So just Let the player do the distance check, and send a message to those Ai which 
/// in a reaction range.
///     Another reason to use a AiInformationReciver patten, because it will be much more
/// convinent to do something like LOD Ai, Like a Army system. The Headquarters will make
/// Decision, and soldiers get the imformation, based on those information, each soldier can make it's
/// Own decision and behaviour, Instead of do everything by its own...... 
///                                     ----------Wei,Zhu Auguest 25/ 2017
/// </summary>
public interface AiInfomationReciver{

    //This function will be called by the LPlayer
    void StartAiBehavior();

    void StopAiBehavior();
}
