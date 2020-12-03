import { Component } from '@angular/core';
import {Hero} from 'src/app/Entities/hero';
import {HeroService} from 'src/app/services/hero/hero.service';
//import {ROOT_URL} from 'config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'Tour of Heroes';
  heroes: Hero[];
  name: string;

  constructor() {
    //console.log(heroService.getHeroes());
  }

  ngOnInit() {
    
  }
}
