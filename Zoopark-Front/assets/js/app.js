const mainZoo = document.querySelector('.mainZoo'),
      cardZoo=document.querySelector('.card'),
      urlZoo='https://localhost:44311/api/ZooPark';

fetch(urlZoo)
    .then(res=>res.json())
    .then(data=>{
       data.forEach(ele => {
          mainZoo.innerHTML+=
          `
          <div class="card" style="width: 18rem;">
          <img src="${ele.imageUrl}" class="card-img-top" alt="...">
          <div class="card-body">
            <h5 class="card-title">No:${ele.id} Ad:${ele.name}</h5>   
          </div>
          <ul class="list-group list-group-flush">
            <li class="list-group-item">Reng:${ele.color}</li>
            <li class="list-group-item">Cins:${ele.gender}</li>
            
          </ul>
          <div class="card-body">
            <a href="#" class="card-link">Qebul vaxti:${ele.acceptedTime}</a>
            
          </div>
      </div>
          `
       });
    })      

