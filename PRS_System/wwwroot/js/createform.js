function ChangeTopic(index) {
    var topic1 = document.getElementById("topicname1");
    var topic2 = document.getElementById("topicname2");
    if (index == 1) {
        topic1.innerHTML = document.getElementById("name_select1");
    }
    else if  (index == 2) {
        topic2.innerHTML = document.getElementById("name_select2");
    }
}