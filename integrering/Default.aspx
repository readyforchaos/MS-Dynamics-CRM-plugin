<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Konfekt AS</title>
    <link href="css/hjem.css" rel="stylesheet" />
    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen" />
</head>
<body>
    <form>
    <div id="container">

        <div id="content">
            <header>
                <img src="img/logo.png" />
                <nav>
                    <ul class="nav nav-pills">
                        <li class="active">
                            <a href="default.aspx"><i class="icon-home"></i>Hjem</a>
                        </li>
                        <li><a href="klageskjema.aspx"><i class="icon-envelope"></i>Henvendelse</a></li>
                    </ul>
                </nav>
            </header>
            <div class="hero-unit main-unit">
                <div class="span10">
                    <div class="well">
                        <div class="jumbotron">
                            <h3>Historien bak selskapet</h3>
                            <div class="span6">
                                <p class="lead">Vi lager den beste sjokoladen i Norge! Sjokolade er et næringsmiddel der de viktigste ingrediensene er kakaomasse, kakaofett og sukker. Sjokolade er blant verdens mest populære godterier. </p>
                            </div>
                            <img class="skaft" src="img/skaft.png" /><br />
                            <a class="btn btn-large btn-primary" href="omoss.html">Les mer</a>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="js/bootstrap.min.js"></script>
    </form>
</body>
</html>
