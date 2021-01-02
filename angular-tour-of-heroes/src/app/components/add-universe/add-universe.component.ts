import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

import { Hero } from 'src/app/Entities/hero';
import { ListItem } from 'src/app/Entities/listitem';
import { Universe } from 'src/app/Entities/universe';
import { UniverseService } from 'src/app/services/universe/universe.service';

@Component({
  selector: 'app-add-universe',
  templateUrl: './add-universe.component.html',
  styleUrls: ['./add-universe.component.css']
})
export class AddUniverseComponent implements OnInit 
{
  newUniverse: Universe = new Universe();
  newUniverseId: number;

  constructor(public dialogRef: MatDialogRef<AddUniverseComponent>, private universeService: UniverseService) { }

  ngOnInit(): void { }

  saveFunction() 
  {
    this.newUniverse.universeId = 0;
    this.universeService
        .putUniverse(this.newUniverse)
        .subscribe(universes => {
          this.newUniverseId = universes;
          this.dialogRef.close(this.newUniverseId);
        });
  }

  closeModal() 
  {
    this.dialogRef.close(-1);
  }
}
