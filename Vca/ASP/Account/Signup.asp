<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Vca - Sign Up</title>
    <link rel="stylesheet" href="~/../../../lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/../../../css/site.css" />
    <link rel="stylesheet" href="~/../../../css/account.css" />
</head>
<body>
    <header>
    </header>
    <div class="container">
        <main role="main" class="pb-3">
            <section class="ftco-section">
                <div class="row justify-content-center">
                    <div class="col-md-7 col-lg-5">
                        <div class="login-wrap p-4 p-md-5">
                            <div class="icon d-flex align-items-center justify-content-center">
                                <span class="fa fa-user-o"></span>
                            </div>
                            <h3 class="text-center mb-4">Sign Up</h3>
                            <form action="../../../Account/Signup" method="post" class="login-form">
                                <div class="form-group my-3">
                                    <input type="text" name="Email" class="form-control rounded-left" placeholder="Email" required>
                                </div>
                                <div class="form-group my-3">
                                    <input type="text" name="FirstName" class="form-control rounded-left" placeholder="FirstName" required>
                                </div>
                                <div class="form-group my-3">
                                    <input type="text" name="LastName" class="form-control rounded-left" placeholder="LastName" required>
                                </div>
                                <div class="form-group d-flex my-3">
                                    <input type="password" name="Password" class="form-control rounded-left" placeholder="Password" required>
                                </div>
                                <div class="form-group mt-5">
                                    <button type="submit" class="form-control btn btn-primary rounded submit px-3">Submit</button>
                                </div>
                              
                                <div class="w-50 text-md-right">
                                    <a href="Signin.asp" class="small">Already have an account. Signin...</a>
                                </div>
                                <%
                                    Dim errorDescription
                                    errorDescription = Request.QueryString("errorDescription")
                                    If Len(errorDescription) > 0 Then
                                        Response.Write "<span class = 'text-danger'>" & errorDescription & "</span>"
                                    End If
                                %>

                            </form>
                        </div>
                    </div>
                </div>
            </section>
        </main>
    </div>
   
    <script src="~/../../../lib/jquery/dist/jquery.min.js"></script>
    <script src="~/../../../lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="~/../../../js/site.js"></script>
</body>
</html>
