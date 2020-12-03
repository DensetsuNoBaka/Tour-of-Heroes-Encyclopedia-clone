import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { UseExistingWebDriver } from 'protractor/built/driverProviders';

import {Hero} from 'src/app/Entities/hero';
import {ListItem} from 'src/app/Entities/listitem';
import {HeroService} from 'src/app/services/hero/hero.service';
import { UniverseService } from 'src/app/services/universe/universe.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  heroes: ListItem[];
  universes: ListItem[];
  selectedHero: Hero;
  selectedHeroId: number = 0;
  selectedUniverseId: number = 0;

  constructor(private heroService: HeroService, private universeService: UniverseService ,private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void 
  {    
    this.getHeroes();

    //this.selectedHeroId = +this.route.snapshot.paramMap.get('id');
    
    /*if (this.selectedValue != 0)
    {
      this.heroService
        .getHero(this.selectedValue)
        .subscribe(heroes => {
          this.selectedHero = heroes[0];
        });
    }*/
    this.universeService
        .getUniverseList()
        .subscribe(universes => {
          this.universes = universes;
        });
  }

  getHeroes(): void {
    console.log(this.selectedUniverseId);
    this.heroService
    .getHeroes(this.selectedUniverseId)
    .subscribe(heroes => {
      this.heroes = heroes;
    });
  }

  getHeroesList(): ListItem[]
  {
    /*var heroes: ListItem[];
    if (this.selectedUniverseId != null)
    {
      for (var c = 0; c < this.heroes.length; c++)
      {
        if(heroes[c].)
      }
    } else
    {
      heroes = this.heroes;
    }*/
    return this.heroes;
  }

  onUniverseSelect(e): void
  {
    this.selectedHeroId = null;
    this.selectedHero = null;
    this.heroes = null;
    this.getHeroes();
  }

  onHeroSelect(e): void
  {
    if(this.selectedHeroId != 0)
    {
      this.heroService
        .getHero(this.selectedHeroId)
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
