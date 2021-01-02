import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { UseExistingWebDriver } from 'protractor/built/driverProviders';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';

import { AddUniverseComponent } from 'src/app/components/add-universe/add-universe.component';

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

  constructor(private heroService: HeroService, private universeService: UniverseService ,private router: Router, private route: ActivatedRoute, public matDialog: MatDialog) { }

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

  //This function runs the heroService Get method to update the heroes list in the dropdown
  getHeroes(): void 
  {
    this.heroService
    .getHeroes(this.selectedUniverseId)
    .subscribe(heroes => {
      this.heroes = heroes;
    });
  }

  getHeroesList(): ListItem[]
  {
    return this.heroes;
  }

  //This event function is called when the value of the Universe select dropdown is changed. This function resets the heroes selection and repopulates the Heroes dropdown with the Universe filter
  onUniverseSelect(e): void
  {
    this.selectedHeroId = null;
    this.selectedHero = null;
    this.heroes = null;

    if(this.selectedUniverseId == -1)
    {
      this.openUniverseModal();
    } else
    {
      this.getHeroes();
    }
  }

  //This function is called whenever a new value is selected from the Heroes dropdown
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

  //This function instantiates and opens the Add Universe modal form
  openUniverseModal() 
  {
    const dialogConfig = new MatDialogConfig();
    
    dialogConfig.disableClose = true; // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "universe-modal-component";
    dialogConfig.height = "350px";
    dialogConfig.width = "600px";
    dialogConfig.data = {
    newUniverseId: this.selectedUniverseId
  }
    
    let modalDialog = this.matDialog.open(AddUniverseComponent, dialogConfig);

    modalDialog.afterClosed().subscribe((result) => {
      if(result != -1)
      {
        this.universeService
        .getUniverseList()
        .subscribe(universes => {
          this.universes = universes;
          this.selectedUniverseId = result;
          this.getHeroes();
        });
      }
      else
      {
        this.selectedUniverseId = 0;
        this.getHeroes();
      }      
    });
  }
}
