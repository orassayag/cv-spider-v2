<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WallaSearch.aspx.cs" Inherits="WallaSearch" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="jquery.min.js" type="text/javascript"></script>
    <script src="jquery.timer.js" type="text/javascript"></script>
    <script type="text/javascript">
        var Example1 = new (function () {

            // Stopwatch element on the page
            var $stopwatch;

            // Timer speed in milliseconds
            var incrementTime = 70;

            // Current timer position in milliseconds
            var currentTime = 0;

            // Output time and increment
            function updateTimer() {
                var timeString = formatTime(currentTime);
                $stopwatch.html(timeString);
                currentTime += incrementTime;
            }

            // Start the timer
            $(function () {
                $stopwatch = $('#stopwatch');
                Example1.Timer = $.timer(updateTimer, incrementTime, true);
            });
        });

        // Common functions
        function pad(number, length) {
            var str = '' + number;
            while (str.length < length) { str = '0' + str; }
            return str;
        }
        function formatTime(time) {
            time = time / 10;
            min = parseInt(time / 6000),
            sec = parseInt(time / 100) - (min * 60),
            hundredths = pad(time - (sec * 100) - (min * 6000), 2);
            return (min > 0 ? pad(min, 2) : "00") + ":" + pad(sec, 2) + ":" + hundredths;
        }

        $(function () {
            var type = 2;
            var id = '74143';
            if (type == 1) {
                for (i = 0; i < 900; i++) {
                    var postDataStatus = 'i=' + $('#total').html();
                    $.ajax({
                        type: "POST",
                        url: "NewFetchMails.ashx",
                        data: postDataStatus,
                        success: function (msg) {
                            $('#total').html(msg);
                        }
                    });
                }
            }
            else {
                var postDataStatus = 'i=' + id;
                $.ajax({
                    type: "POST",
                    data: postDataStatus,
                    url: "PrintMails.ashx",
                    success: function (msg) { }
                });
            }
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <span id="stopwatch">00:00:00:00</span><br />
        <span id="total">0</span>
    </form>
</body>
</html>
