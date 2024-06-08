using EtherEngine.Components;
using EtherEngine.Components.Graphics;
using EtherEngine.Utils.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Managers
{
    public class AnimationCatalog //TODO: Make disposable.
    {
        private EtherEntity _parentEntity;

        public event EventHandler CurrentAnimationChanged;

        private Dictionary<string, SpriteAnimationComponent> _animations = new Dictionary<string, SpriteAnimationComponent>();
        private Dictionary<string, SpriteComponent> _sprites = new Dictionary<string, SpriteComponent>();
        public string CurrentAnimation { get; private set; }

        public AnimationCatalog(EtherEntity parentEntity)
        {
            _parentEntity = parentEntity;
        }

        public void Add(in SpriteComponent sprite, in SpriteAnimationComponent animation)
        {
            _animations.Add(animation.Name, animation);
            _sprites.Add(animation.Name, sprite);
            if (CurrentAnimation == default || CurrentAnimation == "")
                Play(animation.Name);
        }

        public void Remove(in SpriteAnimationComponent animation)
        {
            _animations.Remove(animation.Name);
            _sprites.Remove(animation.Name);
        }

        public void Remove(string name)
        {
            _animations.Remove(name);
            _sprites.Remove(name);
        }

        public void Play(string AnimtionName)
        {
            if (AnimtionName != CurrentAnimation)
            {
                CurrentAnimation = AnimtionName;
                _parentEntity.ReplaceComponent(_animations[CurrentAnimation]);
                _parentEntity.ReplaceComponent(_sprites[CurrentAnimation]);
                Utils.EventUtils.Invoke(CurrentAnimationChanged, this, new EventArgs());
            }
        }

        public void Pause()
        {
            ref var animation = ref _parentEntity.GetComponent<SpriteAnimationComponent>();
            animation.IsPaused = true;
        }

        public void Unpause()
        {
            ref var animation = ref _parentEntity.GetComponent<SpriteAnimationComponent>();
            animation.IsPaused = false;
        }
    }

    public class AnimationManager : LazySingleton<AnimationManager>
    {
        private Dictionary<Guid, AnimationCatalog> _registry = new Dictionary<Guid, AnimationCatalog>();

        private AnimationManager()
        {
        }

        public void Register(EtherEntity entity)
        {
            if (!_registry.ContainsKey(entity.GetUid()))
                _registry.Add(entity.GetUid(), new AnimationCatalog(entity));
        }

        public void Unregister(EtherEntity entity)
        {
            if (_registry.ContainsKey(entity.GetUid()))
            {
                //var animation = _registry[entity.GetUid()];
                //_registry[entity.GetUid()] = null;
                //animation.Dispose();
                _registry.Remove(entity.GetUid());
            }

        }

        public void Add(EtherEntity entity, in SpriteComponent sprite, in SpriteAnimationComponent animation) => _registry[entity.GetUid()].Add(sprite, animation);

        public void Remove(EtherEntity entity, in SpriteAnimationComponent animation) => _registry[entity.GetUid()].Remove(animation);

        public void Remove(EtherEntity entity, string name) => _registry[entity.GetUid()].Remove(name);

        public void Play(EtherEntity entity, string animationName) => _registry[entity.GetUid()].Play(animationName);

        public void Pause(EtherEntity entity) => _registry[entity.GetUid()].Pause();

        public void Unpause(EtherEntity entity) => _registry[entity.GetUid()].Unpause();

    }
}
