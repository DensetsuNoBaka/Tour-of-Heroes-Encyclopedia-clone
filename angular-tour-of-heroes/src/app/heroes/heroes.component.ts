import { Component, OnInit } from '@angular/core';
import {Hero} from 'src/app/Entities/hero';
import {HeroService} from 'src/app/Heroes/hero.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  heroes: Hero[];
  constructor(private heroService: HeroService) { }

  ngOnInit(): void {
    this.heroService
        .getHeroes()
        .subscribe(heroes => {
          this.heroes = heroes;
        });

    this.heroService
        .putHero()
        .subscribe(heroes => {
          //this.heroes = heroes;
          console.log("Success");
        });
  }

}
