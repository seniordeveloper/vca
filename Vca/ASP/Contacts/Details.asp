<%
Response.LCID = 1091
%>
<!--#include virtual = "/ASP/jsonObject.class.asp" -->
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Vca - Contact details</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="stylesheet" href="~/../../../lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/../../../css/site.css" />
</head>
<body>
    <%
            Dim loggedinUser
            loggedinUser=Request.Cookies("AccountDetails")

            Dim jObject
            Set jObject = new JSONobject

            Dim userFirstName
            Dim userLastName
  
            Dim outputUser
            
            If Len(loggedinUser) > 0 Then
              Set outputUser = jObject.parse(loggedinUser)
  
              userFirstName = outputUser("FirstName")
              userLastName = outputUser("LastName")
            Else
              Response.Redirect("/Account/Signin")
            End If
    %>
    <header>
        <nav class="navbar navbar-expand-lg bg-body-tertiary">
            <div class="container-fluid">
              <a class="navbar-brand" href="#">Vca </a>
              <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText" aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
              </button>
              <div class="collapse navbar-collapse" id="navbarText">
                <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                  <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="/Contacts/Index">My Contacts</a>
                  </li>
                </ul>
                <span class="navbar-text">
                  Welcome <span><%= userFirstName%></span>&nbsp;<span><%= userLastName%></span>
                </span> &nbsp;<a href="/Account/Signout"><i class="fa fa-sign-out" aria-hidden="true"></i></a>
              </div>
            </div>
          </nav>
    </header>
    <div class="container">
        <main role="main" class="pb-3">
           
            <div class="row">
                <div class="col-12">
                    <div class="d-block m-auto w-50 mt-3">
                        <%
                            Dim errorDescription
                            errorDescription = Request.QueryString("errorDescription")
                            If Len(errorDescription) > 0 Then
                                Response.Write "<span class = 'text-danger'>" & errorDescription & "</span>"
                            End If

                            Dim info
                            info = Request.QueryString("info")
                            If Len(info) > 0 Then
                                Response.Write "<div class = 'text-success text-center fs-2'>" & info & "</div>"
                            End If

                            Dim myContact
                            myContact=Request.Cookies("ContactDetails")
                        
                            Dim outputObj
                            Set outputObj = jObject.parse(myContact)
                            
                            Dim Name
                            Dim PhoneNumber

                            If Len(myContact) > 0 Then
                                Name = outputObj("Name")
                                PhoneNumber = outputObj("PhoneNumber")
                            End If
                        %>
                         <h4>Contact details <span><%= Name%></span></h4>
                         <hr />
                        <dl><dt>Name</dt><dd><%= Name%></dd><dt>Phone number</dt><dd><%= PhoneNumber%></dd></dl>
                         <div class="mt-2">
                            <a href="../../../Contacts/Index">Back to List</a>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    </div>

    <script src="~/../../../lib/jquery/dist/jquery.min.js"></script>
    <script src="~/../../../lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="~/../../../js/site.js"></script>
</body>

</html>