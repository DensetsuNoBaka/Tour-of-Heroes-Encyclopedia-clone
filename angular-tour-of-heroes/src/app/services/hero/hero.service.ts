import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http'; 

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import config from 'config.json';
import {Hero} from 'src/app/Entities/hero';
import {ListItem} from 'src/app/Entities/listitem';
import { JsonPipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class HeroService {
    
  constructor(public http:HttpClient)  
  {

  }

  getHeroes(universeId: number)
  {
    var opts = new HttpParams();
    var heroes = new Hero();
    if(universeId != 0) { opts = opts.append("universeId", `${universeId}`); }

    return this.http.get<ListItem[]>(`${config.api_url}Heroes/List`, {params: opts}).pipe(map(data => {
      let heroes: ListItem[];
      heroes = <ListItem[]>JSON.parse(data.toString());

      return heroes;
    }));

    /*return this.http.get<Hero[]>(`${config.api_url}Heroes/Get`).pipe(map(data => {
      let heroes: Hero[];
      heroes = <Hero[]>JSON.parse(data.toString());

      return heroes;
    }));*/
  }

  getHero(heroId: number)
  {
    var opts = new HttpParams();
    var heroes = new Hero();
    opts = opts.append("heroId", `${heroId}`);

    return this.http.get<Hero[]>(`${config.api_url}Heroes/Get`, {params: opts}).pipe(map(data => {
      let heroes: Hero[];
      heroes = <Hero[]>JSON.parse(data.toString());
      return heroes;
    }));
  }

  putHero(hero: Hero)//: Observable<void>
  {
    var opts = new HttpParams()// { params: new HttpParams({fromString: "_heroID = 1"}) };
    
    opts = opts.append("heroId", "2");
    /*var hero = new Hero();
    hero.heroName = "All Might";
    hero.powerLevel = "Mountain";
    hero.pictureUrl = "https://static.wikia.nocookie.net/bokunoheroacademia/images/c/cd/Toshinori_Yagi_Golden_Age_Hero_Costume_%28Anime%29.png/revision/latest?cb=20190129015644";
    var body = JSON.stringify(hero);
    */
    //let hero = { "heroName":"All Might", "powerLevel":"Mountain", "pictureUrl": "https://static.wikia.nocookie.net/bokunoheroacademia/images/c/cd/Toshinori_Yagi_Golden_Age_Hero_Costume_%28Anime%29.png/revision/latest?cb=20190129015644"}

    //return this.http.get<string>(`${config.api_url}Heroes/Get`, {params: opts}).pipe(map(data => data));
    //return this.http.put(`${config.api_url}Heroes/Put`, hero).pipe(map(data => data));
    return this.http.put<Hero>(`${config.api_url}Heroes/Put`, hero)
      .pipe(map(data => data));
  }

  /*AddEmployee(emp:employee)  
  {  
    
    const headers = new HttpHeaders().set('content-type', 'application/json');  
    var body = {  
                      Fname:emp.Fname,Lname:emp.Lname,Email:emp.Email  
              }  
    
  return this.http.post<employee>(ROOT_URL+'/Employees',body,{headers})  
    
  }  
    
  EditEmployee(emp:employee)  
  {  
      const params = new HttpParams().set('ID', emp.ID);  
    const headers = new HttpHeaders().set('content-type', 'application/json');  
    var body = {  
                      Fname:emp.Fname,Lname:emp.Lname,Email:emp.Email,ID:emp.ID  
              }  
          return this.http.put<employee>(ROOT_URL+'/Employees/'+emp.ID,body,{headers,params})  
    
  }  
    
    
    
  DeleteEmployee(emp:employee)  
  {  
      const params = new HttpParams().set('ID', emp.ID);  
    const headers = new HttpHeaders().set('content-type', 'application/json');  
    var body = {  
                      Fname:emp.Fname,Lname:emp.Lname,Email:emp.Email,ID:emp.ID  
              }  
          return this.http.delete<employee>(ROOT_URL+'/Employees/'+emp.ID)  
    
  }*/  
}
