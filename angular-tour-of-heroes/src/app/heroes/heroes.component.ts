import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';

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

  constructor(private heroService: HeroService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void 
  {    
    this.getHeroes();

    this.selectedValue = +this.route.snapshot.paramMap.get('id');
    console.log(this.selectedValue);
    if (this.selectedValue != 0)
    {
      this.heroService
        .getHero(this.selectedValue)
        .subscribe(heroes => {
          this.selectedHero = heroes[0];
        });
    }
  }

  getHeroes(): void {
    this.heroService
    .getHeroes()
    .subscribe(heroes => {
      this.heroes = heroes;
    });
  }

  onHeroSelect(e): void
  {
    if(this.selectedValue != 0)
    {
      this.heroService
        .getHero(this.selectedValue)
        .subscribe(heroes => {
          this.selectedHero = heroes[0];
        });
    } else
    {
      this.selectedHero = null;
    }
    

    //this.router.navigateByUrl(`heroes/${this.selectedValue}`);
  }
}
