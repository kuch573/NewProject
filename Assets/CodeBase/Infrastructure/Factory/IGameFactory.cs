using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory:IService
  {
    List<ISavedProgress> ProgressWriters { get; }
    List<ISavedProgressReader> ProgressReaders { get; }
    GameObject CreateHero(Vector3 at);
    GameObject CreateHud();
    GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
    void CreateSpawner( string spawnerId, Vector3 at, MonsterTypeId monsterTypeId);
    void Cleanup();
  }
}