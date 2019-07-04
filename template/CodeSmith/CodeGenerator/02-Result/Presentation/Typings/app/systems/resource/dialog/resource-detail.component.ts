﻿import { Component, Injector } from '@angular/core';
import { env } from '../../env';
import { DialogEditComponentBase } from '../../../util';
import { ResourceViewModel } from './model/resource-view-model';

/**
 * 资源详情页
 */
@Component({
    selector: 'resource-detail',
    templateUrl: !env.dev() ? './html/detail.component.html' : '/view/systems/resource/detail'
})
export class ResourceDetailComponent extends DialogEditComponentBase<ResourceViewModel> {
    /**
     * 初始化资源详情页
     * @param injector 注入器
     */
    constructor(injector: Injector) {
        super(injector);
    }

    /**
     * 获取基地址
     */
    protected getBaseUrl() {
        return "resource";
    }
    
    /**
     * 是否远程加载
     */
    isRequestLoad() {
        return false;
    }
}