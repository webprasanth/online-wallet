$(document).ready(function(){$.ajax({type:"GET",url:"/api/User/Dashboard",dataType:"json"}).done(function(t){var o=t.transfersCount,a=t.depositsCount,e=t.withdrawalsCount,n=t.incomes.toFixed(2),r=t.outcomes.toFixed(2);0===o&&0===a&&0===e?($("#charts").remove(),$("body").append('<div class="col-md-12"><h2><center>To display charts you need to do some transaction</center></h2></div>')):(createTDWchart(o,a,e),createIOchart(n,r))}).fail(function(t,o,a){alert("Cannot load history")})});