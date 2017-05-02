function check(input) {
    if (input.value != document.getElementById('password').value) {
        document.getElementById('confirmPasswordspan').innerHTML = 'Password must be matching.';
        input.setCustomValidity('Password Must be Matching.');
    } else {
        // input is valid -- reset the error message
        document.getElementById('confirmPasswordspan').innerHTML = '';
        input.setCustomValidity('');
    }
}