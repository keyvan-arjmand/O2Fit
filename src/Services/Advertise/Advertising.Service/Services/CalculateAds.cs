using Advertising.Domain.Entities.Advertise;
using System;
using System.Collections.Generic;
using System.Text;

namespace Advertising.Service.Services
{
    public static class CalculateAds
    {
        public static Advertise Calulate(Advertise advertise, bool Click, bool View, bool Hint)
        {
            Advertise _ads = advertise;

            if (_ads.ChargeAmount > 0)
            {
                if (Click)
                {
                    double _charge = _ads.ChargeAmount - _ads.ClickPrice;
                    _ads.ChargeAmount = _charge > 0 ? _charge : 0;
                    _ads.ClickCount = _ads.ClickCount + 1;
                }
                else if (View)
                {
                    double _charge = _ads.ChargeAmount - _ads.ViewPrice;
                    _ads.ChargeAmount = _charge > 0 ? _charge : 0;
                    _ads.ViewCount = _ads.ViewCount + 1;
                }
                else if (Hint)
                {
                    _ads.HintCount = _ads.HintCount + 1;
                }
            }
           
            if(_ads.ChargeAmount == 0)
            {
                _ads.IsActive = false;
            }

            return _ads;

        }
    }
}
