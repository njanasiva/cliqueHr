import { BrowserModule } from '@angular/platform-browser';
import { NgModule, COMPILER_OPTIONS, CompilerFactory, Compiler, Injector } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CliqueHRInterceptorService } from 'src/Application/Services/clique-hrinterceptor.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { DpDatePickerModule } from 'ng2-date-picker';
import { ɵb,ɵi,ɵf,ɵg,ɵa,ɵe,ɵd,ɵh,ɵc } from 'ng2-date-picker';
import { CliqueAuthGuardService } from 'src/Application/Services/clique-auth-guard.service';
import { JitCompilerFactory } from '@angular/platform-browser-dynamic';
import { AppSetings } from 'src/Application/Loader/runtime-function';

export function createCompiler(fn: CompilerFactory): Compiler {     
  return fn.createCompiler();
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DpDatePickerModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CliqueHRInterceptorService,
      multi: true
    },
    ɵb,ɵi,ɵf,ɵg,ɵa,ɵe,ɵd,ɵh,ɵc,
    CliqueAuthGuardService,
    {
      provide: COMPILER_OPTIONS,
      useValue: {},
      multi: true
    },
    {
      provide: CompilerFactory,
      useClass: JitCompilerFactory,
      deps: [COMPILER_OPTIONS]
    },
    {
      provide: Compiler,
      useFactory: createCompiler,
      deps: [CompilerFactory]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private injector: Injector) {
    AppSetings.setInjector = injector; 
   }
 }
