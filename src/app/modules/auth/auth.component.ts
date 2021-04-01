import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { TranslationService } from '../i18n/translation.service';
interface LanguageFlag {
  lang: string;
  name: string;
  flag: string;
  active?: boolean;
}
@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})

export class AuthComponent implements OnInit {

  today: Date = new Date();
  language: LanguageFlag;
  languages: LanguageFlag[] = [
    {
      lang: 'en',
      name: 'English',
      flag: './assets/media/svg/flags/226-united-states.svg',
    },
    {
      lang: 'ar',
      name: 'Arabic',
      flag: './assets/media/svg/flags151-united-arab-emirates.svg',
    }
  ];
  constructor(
    private translationService: TranslationService,
    private router: Router,
    @Inject(DOCUMENT) private document: Document
  ) { }

  ngOnInit() {
    this.setSelectedLanguage();
    this.changeCssFile(this.translationService.getSelectedLanguage());
    // this.router.events
    //   .pipe(filter((event) => event instanceof NavigationStart))
    //   .subscribe((event) => {
    //     this.setSelectedLanguage();
    //   });
  }

  setLanguageWithRefresh(lang) {
    this.changeCssFile(lang);
    this.setLanguage(lang);
    window.location.reload();
  }

  setLanguage(lang) {
    this.languages.forEach((language: LanguageFlag) => {
      if (language.lang === lang) {
        language.active = true;
        this.language = language;
      } else {
        language.active = false;
      }
    });
    this.translationService.setLanguage(lang);
  }

  setSelectedLanguage(): any {
    this.setLanguage(this.translationService.getSelectedLanguage());
  }

  changeCssFile(lang: string) {
    let htmlTag = this.document.getElementsByTagName('html')[0] as HTMLHtmlElement;
    htmlTag.dir = lang === 'ar' ? 'rtl' : 'ltr';
    let headTag = this.document.getElementsByTagName('head')[0] as HTMLHeadElement;
    headTag.dir = lang === 'ar' ? 'rtl' : 'ltr';
    let existingLink = this.document.getElementById('langCss') as HTMLLinkElement;
    let bundleName = lang === 'ar' ? 'arabicStyle.css' : 'englishStyle.css';
    if (existingLink) {
      existingLink.href = bundleName;
    } else {
      let newLink = this.document.createElement('link');
      newLink.rel = 'stylesheet';
      newLink.type = 'text/css';
      newLink.id = 'langCss';
      newLink.href = bundleName;
      headTag.appendChild(newLink);
    }
  }
}
