﻿namespace OdiApp.EntityLayer.Base
{
    public class StringBaseModel : BaseAuditModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
