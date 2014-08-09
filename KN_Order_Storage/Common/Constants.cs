using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_Order_Storage.Common
{
    public class WebConstants
    {
        public const string ClientSourceAdd = "ClientSource";
        public const string ClientStatusYes = "Y";
        public const string ClientStatusNo = "N";

        public const string SearchAll = "所有";
        public const string SearchOther = "其他";
        public const string ClientReachStatusN = "未到店";
        public const string ClientReachStatusR = "已到店";
        public const string ClientOrderStatusN = "未下单";
        public const string ClientOrderStatusO = "已下单";
        public const string ClientOrderStatusD = "已结单";

        public const string OrderTypeCST = "订做";
        public const string OrderTypeSpot = "现货";
        public const string OrderStatusO = "下单";
        public const string OrderStatusR = "到货";
        public const string OrderStatusG = "已取";

    }
}