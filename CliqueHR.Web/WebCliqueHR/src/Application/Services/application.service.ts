import { Injectable, Renderer2, RendererFactory2, ComponentFactoryResolver } from '@angular/core';
import * as CliqueHRType from '../Types/types.api'
@Injectable()
export class ApplicationService extends CliqueHRType.WebAppService.AbstractApplicationService {

  constructor(rendererFactory: RendererFactory2,
    protected componentFactory: ComponentFactoryResolver) {
    super(rendererFactory.createRenderer(null, null), componentFactory);
  }
}
