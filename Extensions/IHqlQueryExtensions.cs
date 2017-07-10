using Orchard.ContentManagement;
using Orchard.Projections.Models;
using Orchard.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Utility.Extensions
{
    public static class IHqlQueryExtensions
    {
        public static IHqlQuery DateFilter(this IHqlQuery query, string partName, string fieldName, DateTime? from, DateTime? to)
        {
            var propertyName = String.Join(".", partName, fieldName, "");
            return query.Where(
                alias => alias.ContentPartRecord<FieldIndexPartRecord>().Property("IntegerFieldIndexRecords", propertyName.ToSafeName()),
                predicate => predicate.And(
                    x => x.Eq("PropertyName", propertyName),
                    y =>
                    {
                        if (from.HasValue && to.HasValue)
                        {
                            y.And(
                                date => date.Ge("Value", from.Value.Ticks),
                                date => date.Le("Value", to.Value.Ticks)
                            );
                        }
                        else if (from.HasValue)
                        {
                            y.Ge("Value", from.Value.Ticks);
                        }
                        else if (to.HasValue)
                        {
                            y.Le("Value", to.Value.Ticks);
                        }
                    }
                )
            );
        }

        public static IHqlQuery DateSort(this IHqlQuery query, string partName, string fieldName, bool desc = false)
        {
            var propertyName = String.Join(".", partName, fieldName, "");
            return query.OrderBy(
                alias => alias.ContentPartRecord<FieldIndexPartRecord>().Property("IntegerFieldIndexRecords", propertyName.ToSafeName()),
                order =>
                {
                    if (desc)
                    {
                        order.Desc("Value");
                    }
                    else
                    {
                        order.Asc("Value");
                    }
                }
            );
        }
    }
}