import { Component, OnInit, Input } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';

import { ListItem } from 'src/app/Entities/listitem';
import { UniverseService } from 'src/app/services/universe/universe.service';
import { Universe } from 'src/app/Entities/universe';
import { stringify } from '@angular/compiler/src/util';

@Component({
  selector: 'app-universe-detail',
  templateUrl: './universe-detail.component.html',
  styleUrls: ['./universe-detail.component.css']
})
export class UniverseDetailComponent implements OnInit {
  universeId: number = 0;
  universe: Universe;
  editFieldsEnabled: boolean = true;
  modifiedUniverse: Universe;

  constructor(private universeService: UniverseService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.universeId = +this.route.snapshot.paramMap.get('id');

    this.universeService
      .getUniverse(this.universeId)
      .subscribe(universes => {
        this.universe = universes;
        this.modifiedUniverse = universes;
      });
  }

  onClick(e): void
  {
    console.log(e.target.class);
    document.getElementById(e.target.id).hidden = true;
    document.getElementById(`edit${e.target.id}`).hidden = false;
    this.modifiedUniverse = this.universe;
  }

  onSave(e): void
  {
    let element = e.target.id.split("-", 1)[0];
    this.editFieldsEnabled = false;

    this.universeService
    .putUniverse(this.universe)
    .subscribe(universes => {
      this.editFieldsEnabled = true;
      this.universe = this.modifiedUniverse;
      this.toggleEditElements(`edit${element}`, element);
    });
  }

  onCancel(e): void
  {
    let element = e.target.id.split("-", 1)[0];
    this.modifiedUniverse = this.universe;

    this.toggleEditElements(`edit${element}`, element);
    /*document.getElementById(element).hidden = false;
    document.getElementById(`edit${element}`).hidden = true;*/
  }

  toggleEditElements(toHide: string, toShow: string)
  {
    document.getElementById(toShow).hidden = false;
    document.getElementById(toHide).hidden = true;
  }

}
