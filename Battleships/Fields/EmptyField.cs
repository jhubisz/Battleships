using Battleships.Enums;
using Battleships.Factories;
using Battleships.Fields.Interfaces;
using System.Collections.Generic;

namespace Battleships.Fields
{
    class EmptyField : IField
    {
        public List<(int x, int y)> Fields { get; set; }
        public FieldsFactory FieldsFactory { get; }

        public EmptyField(FieldsFactory fieldsFactory)
        {
            FieldsFactory = fieldsFactory;
        }

        public void AddPosition(int x, int y)
        {
            if (!CheckIfPositionExists(x, y))
                Fields.Add((x, y));
        }

        public bool CheckIfPositionExists(int x, int y)
        {
            return Fields.Contains((x, y));
        }

        public (FiredShotResult result, IField resultField) CheckHit(int x, int y)
        {
            return (new FiredShotResult { Hit = false, ResultType = FiredShotResultType.ShotMissed }, 
                FieldsFactory.CreateMissedShotMarker(x, y));
        }
    }
}
