import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http'; 

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import config from 'config.json';
import { ListItem } from 'src/app/Entities/listitem';
import {Universe} from 'src/app/Entities/universe';

@Injectable({
  providedIn: 'root'
})
export class UniverseService {

  constructor(public http:HttpClient) { }

  getUniverseList()
  {
    var opts = new HttpParams();
    var universes = new Universe();

    return this.http.get<ListItem[]>(`${config.api_url}Universe/List`).pipe(map(data => {
      let universes: ListItem[];
      universes = <ListItem[]>JSON.parse(data.toString());

      return universes;
    }));
  }

  getUniverse(universeId: number)
  {
    var opts = new HttpParams();
    var universes = new Universe();
    opts = opts.append("universeId", `${universeId}`);

    return this.http.get<Universe>(`${config.api_url}Universe/Get`, {params: opts}).pipe(map(data => {
      let universes: Universe;
      universes = <Universe>JSON.parse(data.toString());
      return universes;
    }));
  }

  putUniverse(universe: Universe)//: Observable<void>
  {
    return this.http.put<number>(`${config.api_url}Universe/Put`, universe)
      .pipe(map(data => data));
  }
}
