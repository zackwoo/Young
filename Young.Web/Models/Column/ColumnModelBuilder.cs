using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.Column
{
    public class ColumnModelBuilder
    {
        public DateColumnModel BuildDateColumn(ColumnModel source)
        {
            var model = new DateColumnModel();
            CopyProperty(model, source);
            SetCode(model);
            model.DatabaseType = System.Data.SqlDbType.DateTime2;
            return model;
        }

        public LineTextColumnModel BuildLineTextColumn(ColumnModel source)
        {
            var model = new LineTextColumnModel();
            CopyProperty(model, source);
            SetCode(model);
            model.Length = 255;
            model.DatabaseType = System.Data.SqlDbType.NVarChar;
            return model;
        }

        public NumberColumnModel BuildNumberColumn(ColumnModel source)
        {
            var model = new NumberColumnModel();
            CopyProperty(model, source);
            SetCode(model);
            model.DatabaseType = System.Data.SqlDbType.Float;
            return model;
        }

        public RichTextColumnModel BuildRichTextColumn(ColumnModel source)
        {
            var model = new RichTextColumnModel();
            CopyProperty(model, source);
            SetCode(model);
            model.DatabaseType = System.Data.SqlDbType.Text;
            return model;
        }

        private void CopyProperty(ColumnModel model, ColumnModel source)
        {
            model.TableCode = source.TableCode;
            model.TableName = source.TableName;
            model.Name = source.Name;
            model.IsRequired = source.IsRequired;
            model.IsNeedCustomValidation = source.IsNeedCustomValidation;
            model.Description = source.Description;
            model.DatabaseType = source.DatabaseType;
            model.CustomValidationRegularExpression = source.CustomValidationRegularExpression;
            model.CustomValidationErrorMessage = source.CustomValidationErrorMessage;
            model.Code = source.Code;
        }

        private void SetCode(ColumnModel model)
        {
            model.Code = Young.Util.Sequence.GetNewSequence(6, "C_");
        }
    }
}