﻿namespace domain;

public interface IUpdatable
{
    public void Update(long currentTime);
    public void SetTimer(ITimer timer);
}