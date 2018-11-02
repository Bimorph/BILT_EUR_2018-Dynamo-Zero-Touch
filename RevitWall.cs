using Autodesk.Revit.DB;
using RevitServices.Persistence;
using RevitServices.Transactions;
using Revit.Elements;
using Wall = Autodesk.Revit.DB.Wall;

namespace ZeroTouchNodes
{
    public class RevitWall
    {
        //RevitWall default constructor - it isn't used but should be declared so its access 
        //level can be changed to private, otherwise an unneeded node will appear in the Dynamo
        //node library
        public RevitWall() { }

        //A simple method to create a wall using the Revit API
        public static Revit.Elements.Element Create(double lenghtInFt, int levelId)
        {
            //Get the active document object
            Document doc = DocumentManager.Instance.CurrentDBDocument;

            //Create new points using the Revit API to build a line
            XYZ ptStart = new XYZ(); //no inputs creates an XYZ at 0,0,0
            XYZ ptEnd = new XYZ(lenghtInFt, 0.0, 0.0);

            //Create a Revit API line to define the location curve of the new wall element
            Line lnLocationCurve = Line.CreateBound(ptStart, ptEnd);

            //Create an ElementId object from the levelId input of the method
            ElementId levelElementId = new ElementId(levelId);

            //Open a new transaction using Dynamo's RevitServices library. Transactions must be 
            //opened when creating, modifying or deleting elements from a Revit document
            TransactionManager.Instance.EnsureInTransaction(doc);

            //Instantiate the new wall element
            Wall newWall = Wall.Create(doc, lnLocationCurve, levelElementId, false);

            //Clos the transction to submit the new element into the document
            TransactionManager.Instance.TransactionTaskDone();

            //Return the new wall element and wrap it in Dynamo's Revit wrpper class and also 
            //create a binding between the node and element in Revit to prevent duplicates and create 
            //a synchorinisation/dynamic interop between the two applications
            return newWall.ToDSType(false);
        }
    }
}
