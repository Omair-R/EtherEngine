using EtherEngine.Motion.Drag;
using Microsoft.Xna.Framework;



namespace EtherEngine.Motion
{
    public class DragMotion : Motion
    {
        private IDrag _drag;
        private DragTypes _dragType;

        public override float MaxVelocity
        {
            get
            {
                return _maxVelocity;
            }
            set
            {
                if (_maxVelocity != value)
                {
                    _maxVelocity = value;
                    UpdateDrag();
                }
            }
        }

        private float _reachTime;
        public float ReachTime
        {
            get
            {
                return _reachTime;
            }
            set
            {
                if (_reachTime != value)
                {
                    _reachTime = value;
                    UpdateDrag();
                }
            }
        }

        public DragMotion(float desiredMaxVelocity,
                          float desiredReachTime,
                          DragTypes dragType = DragTypes.StokesDrag,
                          Axis restrict = Axis.None)
        {
            _dragType = dragType;
            _maxVelocity = desiredMaxVelocity;
            _reachTime = desiredReachTime;
            UpdateDrag();

            _restricedAxis = restrict;
        }


        public override Vector2 MoveWithDirection(Vector2 position, Vector2 motionDirection, GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            SuppressMotion(_restricedAxis);

            _currentVelocity += _drag.ComputeVelocityUpdate(_currentVelocity, motionDirection) * elapsedTime;

            return position + _currentVelocity * elapsedTime;

        }


        private IDrag UpdateDrag() => _drag = _dragType switch
        {
            DragTypes.StokesDrag =>
                 new StokeComputation(MaxVelocity,
                                      ReachTime),
            DragTypes.QuadraticDrag =>
                 new QuadraticComputation(MaxVelocity,
                                          ReachTime),
            _ => null,
        };

    }
}
