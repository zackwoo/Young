using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Young.CustomTable.ColumnType;

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

        public ColumnModel BuildColumnModel(ColumnTypeBase source)
        {
            var model = CreateColumnModel(source);
            model.IsNew = false;
            model.Name = source.Name;
            model.Code = source.Code;
            model.Description = source.Description;
            model.IsNeedCustomValidation = source.IsNeedCustomValidation;
            model.IsRequired = source.IsRequired;
            model.CustomValidationErrorMessage = source.CustomValidationErrorMessage;
            model.CustomValidationRegularExpression = source.CustomValidationRegularExpression;
            model.DatabaseType = source.DatabaseType;

            return model;
        }

        public DateType BuildDateType(DateColumnModel model)
        {
            var result = ColumnTypeFactory.CreateDateType();
            CopyProperty(result, model);
            return result;
        }
        public LineTextType BuildLineTextType(LineTextColumnModel model)
        {
            var result = ColumnTypeFactory.CreateLineTextType();
            CopyProperty(result, model);
            result.DatabaseColumnLength = model.Length;
            return result;
        }
        public RichTextType BuildRichTextType(RichTextColumnModel model)
        {
            var result = ColumnTypeFactory.CreateRichTextType();
            CopyProperty(result, model);
            return result;
        }
        public NumberType BuildNumberType(NumberColumnModel model)
        {
            var result = ColumnTypeFactory.CreateNumberType();
            CopyProperty(result, model);
            return result;
        }

        

        private ColumnModel CreateColumnModel(ColumnTypeBase source)
        {
            if (source is LineTextType)
            {
                return new LineTextColumnModel();
            }
            if (source is RichTextType)
            {
                return new RichTextColumnModel();
            }
            if (source is NumberType)
            {
                return new NumberColumnModel();
            }
            if (source is DateType)
            {
                return new DateColumnModel();
            }
            return null;
        }

        private void CopyProperty(ColumnTypeBase target, ColumnModel source)
        {
            target.Name = source.Name;
            target.IsRequired = source.IsRequired;
            target.IsNeedCustomValidation = source.IsNeedCustomValidation;
            target.Description = source.Description;
            target.DatabaseType = source.DatabaseType;
            target.CustomValidationRegularExpression = source.CustomValidationRegularExpression;
            target.CustomValidationErrorMessage = source.CustomValidationErrorMessage;
            target.Code = source.Code;
        }

        private void CopyProperty(ColumnModel target, ColumnModel source)
        {
            target.TableCode = source.TableCode;
            target.TableName = source.TableName;
            target.Name = source.Name;
            target.IsRequired = source.IsRequired;
            target.IsNeedCustomValidation = source.IsNeedCustomValidation;
            target.Description = source.Description;
            target.DatabaseType = source.DatabaseType;
            target.CustomValidationRegularExpression = source.CustomValidationRegularExpression;
            target.CustomValidationErrorMessage = source.CustomValidationErrorMessage;
            target.Code = source.Code;
        }


        private void SetCode(ColumnModel model)
        {
            model.Code = Young.Util.Sequence.GetNewSequence(6, "C_");
        }
    }
}