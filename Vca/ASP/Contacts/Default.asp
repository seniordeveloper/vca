<%
Response.LCID = 1091 
%>
<!--#include virtual = "/ASP/jsonObject.class.asp" -->
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>MyContacts</title>
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
            <div class="row mt-5">
                <div class="col-12">
                    <div class="d-flex justify-content-end"><a class="btn btn-primary" href="Create.asp" role="button">Add new contact</a></div>
                </div>
            </div>
            <div class='row'>
                <div class='col-12'>
                    <table class='table table-striped'>
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Phone number</th>
                                <th>Actions</th>
                                
                            </tr>
                        </thead>
                        <tbody>
            <%
                Dim myContacts
                myContacts=Request.Cookies("ContactsDefault")
               
                If Len(myContacts) > 0 Then
                    
                    Dim outputObj
                    Set outputObj = jObject.parse(myContacts)

                    Dim contact
                    Dim Name
                    Dim PhoneNumber
                    Dim Id
                    
                    For Each contact in outputObj.items
                        Name = contact.value("Name")
                        PhoneNumber = contact.value("PhoneNumber")
                        Id = contact.value("Id")

                        Response.Write("<tr><td><a href=" & "/Contacts/Details/" & Id & ">" & Name & "</a></td><td>" & PhoneNumber & "</td><td><button class='remove-contact btn btn-danger' data-id=" & Id &"><i class='fa fa-trash'></i></button><a class='btn btn-primary ms-1' href=" & "/Contacts/Edit/" & Id & "><i class='fa fa-pen'></i></a></td></tr>")    
                    Next
                Else
                    Response.Redirect("/Contacts/Index")
                End If
            %>
        </tbody>
        </table>
        </div>
        </div>
        </main>

        <div class="modal fade" id="confirmRemovalModal" tabindex="-1" role="dialog" aria-labelledby="confirmRemovalModalTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="confirmRemovalModalTitle">Warning</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  Are you really sure that want to delete this contact?
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-secondary" data-dismiss="modal" id="removalCancel">Cancel</button>
                  <button type="button" class="btn btn-danger" id="submitDeletion">Delete</button>
                </div>
              </div>
            </div>
          </div>
    </div>
        
    <footer class="border-top footer text-muted">
        <div class="container">
            &copy; 2023 - MyContacts
        </div>
    </footer>
    <script src="~/../../../lib/jquery/dist/jquery.min.js"></script>
    <script src="~/../../../lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="~/../../../js/site.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            var $modal = $('#confirmRemovalModal');
            var selectedId = 0;

            $('.remove-contact').on('click', function($eventArgs) {
                $modal.modal('show');
                var sender = $(this);
                selectedId = +sender.data('id');
            });

            $('#removalCancel').on('click', function(sender) {
                $modal.modal('hide');
            });

            $('#submitDeletion').on('click', function(e) {
                if($modal.length > 0) {
                    $modal.modal('hide');
                }

                if(selectedId) {
                    $.ajax({
                        url: '/Contacts/Delete/' + selectedId,
                        type: 'DELETE',
                        success: function(result) {
                            window.location = '/Contacts/Index';
                        }
                    });
                }
            });
        });
    </script>
</body>
</html>
