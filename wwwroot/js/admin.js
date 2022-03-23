const uri = '/user';
let users = [];

const GetAllUsers = () => {
    console.log("in getAllUserFunction")
    fetch(uri, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            }

        })
        .then(response => response.json())
        .then(data =>{
            DisplayUser(data)
          
        }
        )
        .catch(error => console.error('Unable to get Users.', error));
}

    const DisplayUser=(userss)=>
    {
        const tBody = document.getElementById('users');
        tBody.innerHTML = '';
        displayCount(userss.length);

        const button = document.createElement('button');

        userss.forEach(user => {
    
            let deleteButton = button.cloneNode(false);
            deleteButton.innerText = 'Delete';
            deleteButton.setAttribute('onclick', `deleteUser(${user.id})`);
    
            let tr = tBody.insertRow();

            let td1 = tr.insertCell(0);
            let textNode = document.createTextNode(user.userName);
            td1.appendChild(textNode);

            let td2 = tr.insertCell(1);
            let textNode1 = document.createTextNode(user.id);
            td2.appendChild(textNode1);

    
            let td3 = tr.insertCell(2);
            td3.appendChild(deleteButton);

        });
    
        users = userss;
    }
    
    function displayCount(userCount) {
        const name = (userCount === 1) ? 'user' : 'users';
    
        document.getElementById('counter').innerText = `${userCount} ${name}`;
    }
    const addUser =()=>
    {
        const txtAddName = document.getElementById('add-name');
        const txtAddPassword = document.getElementById('add-password');
        const user = {
           UserName: txtAddName.value.trim(),
           Password : txtAddPassword.value.trim(),
           Id:users.length+1
        };
        fetch(`${uri}/AddUser`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")},
                body: JSON.stringify(user)
            })
            .then(response => response.json())
            .then(() => {
                GetAllUsers();
                txtAddName.value = '';
                txtAddPassword.value='';
            })
            .catch(error => console.error('Unable to add user.', error));
    }
    const deleteUser=(id)=>
    {
 fetch(`${uri}/${id}`, {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem("token")
                }
            })
            .then(()=>GetAllUsers())
            .catch(error=>console.error('Unable to delete item.',error));
    }





