using System;

namespace xGraphic
{
    /// <summary>
    /// Определяет, имеет ли графический элемент верхний и нижний элементы
    /// </summary>
    public interface ITopBottomConnector<T>
        where T : Connector
    {
        T BottomConnector { get; set; }
        T TopConnector { get; set; }
    }
}
