import { Component, OnInit } from '@angular/core';
import {Hero} from 'src/app/Entities/hero';
import {ListItem} from 'src/app/Entities/listitem';
import {HeroService} from 'src/app/Heroes/hero.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  heroes: ListItem[];
  selectedHero: Hero;
  selectedValue: number;
  //pie: string;

  constructor(private heroService: HeroService) { }

  ngOnInit(): void 
  {
    //this.pie = "apple";
    
    this.heroService
        .getHeroes()
        .subscribe(heroes => {
          this.heroes = heroes;
        });
  }

  onHeroSelect(e): void
  {
    console.log(e);
    this.heroService
        .getHero(this.selectedValue)
        .subscribe(heroes => {
          this.selectedHero = heroes[0];
        });
  }

  onSaveClick(): void 
  {
    console.log(this.selectedHero);
    this.heroService
        .putHero(this.selectedHero)
        .subscribe(heroes => {
          console.log("Success");
        });
  }

}
