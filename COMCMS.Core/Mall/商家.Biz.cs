using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;

namespace COMCMS.Core
{
    /// <summary>商家</summary>
    public partial class Shop : Entity<Shop>
    {
        #region 对象操作
        static Shop()
        {
            // 累加字段
            //Meta.Factory.AdditionalFields.Add(__.Logins);

            // 过滤器 UserModule、TimeModule、IPModule
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 在新插入数据或者修改了指定字段时进行修正
            if (isNew || Dirtys[__.AddTime]) AddTime = DateTime.Now;

            // 货币保留6位小数
            Latitude = Math.Round(Latitude, 2);
            Longitude = Math.Round(Longitude, 2);
            Balance = Math.Round(Balance, 2);
            AvgScore = Math.Round(AvgScore, 2);
            ServiceScore = Math.Round(ServiceScore, 2);
            SpeedScore = Math.Round(SpeedScore, 2);
            EnvironmentScore = Math.Round(EnvironmentScore, 2);
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化Shop[商家]数据……");

        //    var entity = new Shop();
        //    entity.Id = 0;
        //    entity.ShopName = "abc";
        //    entity.KId = 0;
        //    entity.AId = 0;
        //    entity.Sequence = 0;
        //    entity.Latitude = 0.0;
        //    entity.Longitude = 0.0;
        //    entity.Country = "abc";
        //    entity.Province = "abc";
        //    entity.City = "abc";
        //    entity.District = "abc";
        //    entity.Address = "abc";
        //    entity.Postcode = "abc";
        //    entity.IsDel = 0;
        //    entity.IsHide = 0;
        //    entity.IsDisabled = 0;
        //    entity.Content = "abc";
        //    entity.Keyword = "abc";
        //    entity.Description = "abc";
        //    entity.BannerImg = "abc";
        //    entity.Balance = 0.0;
        //    entity.IsTop = 0;
        //    entity.IsVip = 0;
        //    entity.IsRecommend = 0;
        //    entity.Likes = 0;
        //    entity.AvgScore = 0.0;
        //    entity.ServiceScore = 0.0;
        //    entity.SpeedScore = 0.0;
        //    entity.EnvironmentScore = 0.0;
        //    entity.Pic = "abc";
        //    entity.MorePics = "abc";
        //    entity.Email = "abc";
        //    entity.Tel = "abc";
        //    entity.Phone = "abc";
        //    entity.QQ = "abc";
        //    entity.Skype = "abc";
        //    entity.HomePage = "abc";
        //    entity.Weixin = "abc";
        //    entity.IsShip = 0;
        //    entity.OpenTime = DateTime.Now;
        //    entity.CloseTime = DateTime.Now;
        //    entity.ShippingStartTime = DateTime.Now;
        //    entity.ShippingEndTime = DateTime.Now;
        //    entity.AddTime = DateTime.Now;
        //    entity.Hits = 0;
        //    entity.MyType = 0;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化Shop[商家]数据！");
        //}

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnDelete()
        //{
        //    return base.OnDelete();
        //}
        #endregion

        #region 扩展属性

        private ShopCategory _ShopCategory;
        /// <summary>该文章所对应的栏目</summary>
        public ShopCategory ShopCategory
        {
            get
            {
                //if (_ArticleKind == null && KId > 0 && !Dirtys.ContainsKey("ArticleKind_" + KId))
                //{
                //    _ArticleKind = ArticleKind.Find("Id", KId);
                //    Dirtys["ArticleKind_" + KId] = true;
                //}
                //return _ArticleKind;

                _ShopCategory = ShopCategory.FindById(KId);
                return _ShopCategory;

            }
            set { _ShopCategory = value; }
        }
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static Shop FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            //return Meta.SingleCache[id];

            return Find(_.Id == id);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 业务操作
        #endregion
    }
}