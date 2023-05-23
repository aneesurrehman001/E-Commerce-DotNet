import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IProduct} from "./models/product";
import {IPagination} from "./models/pagination";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'store';

  // TypeScript Syntax ==> variable followed by colon followed by type
  products: IProduct[]=[];
  constructor(private http: HttpClient) {

  }
  ngOnInit(): void {
    /*
    * We are using observables here and inorder to use them we make use of "subscribe".
    * Through it we can get access to our response.
    * Our data is stored in the "response" variable.
    * */
    this.http.get<IPagination>('https://localhost:5001/api/products?pageSize=50').subscribe((response: IPagination) => {
      this.products = response.data;
      //the line below is to use as test to print all products in console.
      console.log(response)
    },error => {
      console.log(error);
    });
  }


}
