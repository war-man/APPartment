﻿using System;

namespace APPartment.Models
{
    public class Audit : Base.Object
    {
        public int Id { get; set; }

        public string TableName { get; set; }

        public DateTime When { get; set; }

        public string KeyValues { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }

        public long HouseId { get; set; }

        public long UserId { get; set; }

        public long? TargetObjectId { get; set; }
    }
}
