import { Component, OnInit } from '@angular/core';
import { ViewChild } from "@angular/core";

declare let $: any;
let name;

@Component({
  selector: 'app-datatable',
  templateUrl: './datatable.component.html',
  styleUrls: ['./datatable.component.css']
})
export class DatatableComponent implements OnInit {

  constructor() { }

  @ViewChild('btnClick') btn;
  @ViewChild('empTable') empTable;

  empDatatable: any;
  buttonClick: any;

  ngOnInit() {

    name = 'victor';

    this.empDatatable = $(this.empTable.nativeElement);
    this.buttonClick = $(this.btn.nativeElement);

    this.empDatatable.dataTable();

    this.buttonClick.click(() => {
      this.buttonClick.hide();
      alert(`hey, ${name} whats is going on`);
    });

  }

}
