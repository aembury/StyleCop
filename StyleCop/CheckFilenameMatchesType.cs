using StyleCop.CSharp;

namespace StyleCop
{
    [SourceAnalyzer(typeof(CsParser))]
    public class CheckFilenameMatchesType : SourceAnalyzer
    {
        private string _filename;

        public override void AnalyzeDocument(CodeDocument currentCodeDocument)
        {
            var codeDocument = (CsDocument)currentCodeDocument;
            if (codeDocument.RootElement != null && !codeDocument.RootElement.Generated)
            {
                _filename = codeDocument.SourceCode.Name;
                codeDocument.WalkDocument(new CodeWalkerElementVisitor<object>(this.InspectCurrentElement), null, null);
            }
        }

        private bool InspectCurrentElement(CsElement element, CsElement parentElement, object context)
        {
            if (element.ElementType == ElementType.Class
                || element.ElementType == ElementType.Interface)
            {
                if (element.Name != _filename)
                {
                    this.AddViolation(element, "CheckFilenameMatchesType", "CatchShouldBeImplemented");
                }
            }
            return true;
        }
    }
}
