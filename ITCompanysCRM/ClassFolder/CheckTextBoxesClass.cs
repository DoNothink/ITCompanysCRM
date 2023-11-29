using System.Collections.Generic;
using System.Windows.Controls;

namespace ITCompanysCRM.ClassFolder
{
    class CheckTextBoxesClass
    {
        public static bool CheckTextBoxes(List<TextBox> textBoxes)
        {
            foreach (var x in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(x.Text))
                {
                    MBClass.ErrorMB($"Заполните обязательное поле");
                    x.Focus();
                    return false;
                }
            }
            return true;
        }
    }
}
