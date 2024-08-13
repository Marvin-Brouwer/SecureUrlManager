﻿namespace SecureUrlManager.App.Features.Registration
{
    public class OpenGraphRequest
    {
        public required Uri Url { get; set; }
        public bool LoadImages { get; set; } = true;
    }
}
