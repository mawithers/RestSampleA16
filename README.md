# RestSampleA16
Sample Code

This software is used to exercise AvaTax A16 REST methods

Run this console App project locally in Visual Studio 2015.
If you have any questions or suggestions, please contact mark.withers@avalara.com.  Feel free to Fork, make changes add and submit back for incorporation in Master branch.

Dependency:
- RavenDB running on localhost ( https://ravendb.net/docs/article-page/3.0/csharp/start/getting-started ).
- NewtonSoft.JSON parser ( http://www.newtonsoft.com/json ).
- VS 2015.
- Excel.

Here is what the App currently does:
- Records, Voids and Unvoids Transactions.
- Process a spreadsheet of Address data to test Lodging Tax. 
- Retrieve a list of Companies.
- Saves Input and Output JSON in RavenDB.

In a future version, the App will:
- Exercise all the methods in the AvaTax 16 API.
- Exercise all the methods in the CertCapture API.
- Exercise all the methods in the Business Communications API.
- Exercise all the methods in the Excise Tax API.
- Exercise all the methods in the Landed Cost API.

